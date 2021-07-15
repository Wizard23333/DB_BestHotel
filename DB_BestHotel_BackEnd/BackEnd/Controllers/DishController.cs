using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackEnd.Models;
using BackEnd;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DishController : ControllerBase
    {
        [HttpPost("UpdateDish")]
        [ApiResponseFilterAttribute]
        //修改菜品
        public DishUpdateResponse Post([FromBody] DishUpdateRequest value)
        {
            try
            {
                if (DataAccess.IsDishExist(value.dish_id))
                {
                    if (DataAccess.UpdateDish(value))
                    {
                        return new DishUpdateResponse { dish_update = true };
                    }
                    else
                    {
                        return new DishUpdateResponse { dish_update = false };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        [HttpPost("ReturnAll_Dish")]
        [ApiResponseFilterAttribute]
        //返回所有信息
        public List<DishReturnResponse> Post()
        {
            try
            {
                return DataAccess.ReturnAll_Dish();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }
        [HttpPost("DeleteDish")]
        [ApiResponseFilterAttribute]
        //删除菜品
        public StatusCodeResult Post([FromBody] DishDeleteRequest value)
        {
            try
            {
                if (DataAccess.IsDishExist(value.dish_id) == true)
                {
                    if (DataAccess.DeleteDish(value.dish_id) == "0")
                    {
                        return new StatusCodeResult(404);
                    }
                    else
                    {
                        return new StatusCodeResult(200);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
        }

        [HttpPost("AddDish")]
        [ApiResponseFilterAttribute]
        //添加菜品
        public DishAddResponse Post([FromBody] DishAddRequest value)
        {
            try
            {
                var a = DataAccess.DishAdd(value);
                return new DishAddResponse { dish_id = a };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return null;
                
                
                
        }
    }
}
