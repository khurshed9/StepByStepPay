namespace WebApi.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchaseController(IPurchaseService service) : BaseController
{
    [HttpGet] public async Task<IActionResult> Get([FromQuery] PurchaseFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();

    [HttpPost]
    public async Task<string> Create([FromForm] PurchaseCreateInfo entity)
        => (await service.CreateAsync(entity));
    
    [HttpDelete("{id:int}")] public async Task<IActionResult> Delete([FromRoute] int id)
        => (await service.DeleteAsync(id)).ToActionResult();
}