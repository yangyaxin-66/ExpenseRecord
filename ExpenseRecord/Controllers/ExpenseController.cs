﻿using ExpenseRecord.DataModel;
using ExpenseRecord.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseRecord.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [AllowAnonymous]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseServices;
        public ExpenseController(IExpenseService expenseService)
        {
            _expenseServices = expenseService;
        }

        [HttpGet]
        public async Task<List<ExpenseItem>> GetALL()
        {
            var result = await _expenseServices.GetAsync();
            if (result == null)
            {
                return new List<ExpenseItem>();
            }
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseItem>> PostAsync([FromBody] ExpenseItemCreateRequest toDoItemCreateRequest)
        {
            DateTime currentTime = DateTime.Now;
            var toDoItemDto = new ExpenseItem
            {
                Id  = Guid.NewGuid().ToString(),
                Description = toDoItemCreateRequest.Description,
                Type = toDoItemCreateRequest.Type,
                Amount = toDoItemCreateRequest.Amount,
                CreateTime = currentTime.ToString("yyyyMMdd"),

        };
            await _expenseServices.CreateAsync(toDoItemDto);
            return Created("", toDoItemDto);
        }
        [HttpDelete("{id}")]
        public async Task DeleteAsync(string id)
        {
            await _expenseServices.DeleteAsync(id);
            
        }

    }
}
