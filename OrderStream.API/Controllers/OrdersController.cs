namespace OrderStream.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;
    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        if (orders == null || orders.Count == 0)
        {
            return NotFound();
        }

        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }

        var orderResponse = _mapper.Map<OrderResponse>(order);

        return Ok(orderResponse);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] OrderRequest orderRequest)
    {
        if (orderRequest == null)
        {
            return BadRequest();
        }

        var order = _mapper.Map<Order>(orderRequest);

        await _orderService.AddOrderAsync(order);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] OrderRequest orderRequest)
    {
        var order = _mapper.Map<Order>(orderRequest);

        await _orderService.UpdateOrderAsync(id, order);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _orderService.DeleteOrderAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return Ok();
    }
}
