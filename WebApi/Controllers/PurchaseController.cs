namespace WebApi.Controllers;

[ApiController]
[Route("api/purchases")]
public class PurchaseController(IPurchaseService service) : BaseController
{

    [HttpPost]
    public async Task<string> Create([FromForm] PurchaseCreateInfo entity)
        => (await service.CreateAsync(entity));
}