using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dto;
using TodoApi.Models;
using TodoApi.Parameters;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _iMapper;

        public TodoController(TodoContext todoContext,IMapper iMapper)
        {
            _iMapper = iMapper;
            _todoContext = todoContext;
        }

        // GET: api/<TodoController>
        [HttpGet]
        //[FromQuery]從網址後面的參數中取值
        public IActionResult Get([FromQuery]TodoSelectParameter value)
        {
            //Include是資料有設外鍵情況下用，如果沒有關聯的話則用join()去關聯，俐：join b in _todoContext.Employee on a.InsertEmployeeId equals b.EmployeeId
            //現階段result類型是IQueryable，會在下面一連串的動作轉化為SQL，如果動作期間使用額外的函式轉換，會無法轉成SQL，程式會報錯
            //建議：在程式做完動作後再用額外的函式轉換
            //錯誤演示：
            //var result = _todoContext.TodoLists.Include(a => a.UpdateEmployee).Include(a => a.InsertEmployee)
            //    .Select(a => ItemToDto(a));
            var result = _todoContext.TodoLists.Include(a => a.UpdateEmployee).Include(a => a.InsertEmployee).Include(a => a.UploadFiles)
                .Select(a => a);

            if (!string.IsNullOrEmpty(value.name))
            {
                //Contains()部分條件符合即可， ==要完全一樣
                result = result.Where(a=> a.Name.Contains(value.name));
            }

            if(value.enable != null)
            {
                result = result.Where(a => a.Enable == value.enable);
            }

            if(value.InsertTime != null)
            {
                //.Date比較年月日
                result = result.Where(a=>a.InsertTime.Date == value.InsertTime);
            }

            if(value.minOrder != null && value.maxOrder != null)
            {
                result= result.Where(a=>a.Orders>=value.minOrder && a.Orders<=value.maxOrder);
            }

            if(result == null || result.Count()==0)
            {
                return NotFound("無此資料");
            }

            return Ok(result.ToList().Select(a => ItemToDto(a)));

            //AutoMapper運用
            //var result = _todoContext.TodoLists.Include(a => a.UpdateEmployee).Include(a => a.InsertEmployee);

            //return _iMapper.Map<IEnumerable<TodoListSelectDto>>(result);
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public TodoListSelectDto Get(Guid id)
        {
            var result = (from a in _todoContext.TodoLists
                          where a.TodoId == id
                          select a)
                          .Include(a => a.UpdateEmployee).Include(a => a.InsertEmployee)
                          .SingleOrDefault();   //如果下Single()沒取到資料程式會當掉 SingleOrDefault()會是null
    

            return ItemToDto(result);
        }

        //From標籤測試
        //有變數在route後面，如：{id}，預設就是[FromRoute]，沒變數的話就是[FromQuery]
        [HttpGet("From/{id}")]
        //因為route中沒有id2變數，所以id2的預設是[FromQuery]
        //參數是class時，預設是[FromBody]使用的是JSON格式
        public dynamic GetFrom([FromRoute]string id, [FromQuery]string id2, [FromBody] string id3, [FromForm]string id4)
        {
            List<dynamic> result = new List<dynamic>();

            result.Add(id);
            result.Add(id2);
            result.Add(id3);
            result.Add(id4);


            return result;

        }



        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        private static TodoListSelectDto ItemToDto(TodoList a)
        {
            //取得子資料
            List<UploadFileDto> updto = new List<UploadFileDto>();
            foreach(var temp in a.UploadFiles)
            {
                UploadFileDto up = new UploadFileDto
                {
                    Name = temp.Name,
                    Src = temp.Src,
                    TodoId = temp.TodoId,
                    UploadFileId = temp.UploadFileId
                };
                updto.Add(up);
            }

            //父資料
            return new TodoListSelectDto
            {
                Enable = a.Enable,
                InsertEmployeeName = a.InsertEmployee.Name,
                InsertTime = a.InsertTime,
                Name = a.Name,
                Orders = a.Orders,
                TodoId = a.TodoId,
                UpdateEmployeeName = a.UpdateEmployee.Name,
                UpdateTime = a.UpdateTime,
                UploadFiles = updto
            };
        }

    }
}
