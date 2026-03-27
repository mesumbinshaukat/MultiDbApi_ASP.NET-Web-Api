using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MultiDbApi.Controllers
{
    public class TableInfo
    {
        public string TableName { get; set; } = string.Empty;
        public string SchemaName { get; set; } = string.Empty;
        public long RowCounts { get; set; }
    }

    [ApiController]
    [Route("api/db")]
    public class DatabaseInfoController : ControllerBase
    {
        private readonly ElasticPoolDbContext _elasticContext;
        private readonly SqlServerDbContext _sqlContext;

        public DatabaseInfoController(ElasticPoolDbContext elasticContext, SqlServerDbContext sqlContext)
        {
            _elasticContext = elasticContext;
            _sqlContext = sqlContext;
        }

        private const string TableDiscoveryQuery = @"
            SELECT 
                t.NAME AS TableName,
                s.Name AS SchemaName,
                p.rows AS RowCounts
            FROM 
                sys.tables t
            INNER JOIN      
                sys.indexes i ON t.OBJECT_ID = i.object_id
            INNER JOIN 
                sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
            INNER JOIN 
                sys.schemas s ON t.schema_id = s.schema_id
            WHERE 
                t.is_ms_shipped = 0
                AND i.index_id IN (0,1)
            GROUP BY 
                t.NAME, s.Name, p.Rows";

        /// <summary>
        /// Fetches all available physical tables from both databases with their row counts.
        /// </summary>
        // GET: api/db/tables
        [HttpGet("tables")]
        public async Task<IActionResult> GetTables()
        {
            try
            {
                var elasticTables = await _elasticContext.Database
                    .SqlQueryRaw<TableInfo>(TableDiscoveryQuery)
                    .ToListAsync();

                var sqlTables = await _sqlContext.Database
                    .SqlQueryRaw<TableInfo>(TableDiscoveryQuery)
                    .ToListAsync();

                return Ok(new
                {
                    ElasticPoolInfo = elasticTables,
                    SqlServerInfo = sqlTables
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to fetch tables", message = ex.Message });
            }
        }
    }
}
