using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CodingMilitia.PlayBall.WebFrontend.BackForFront.Web.Features.Groups
{
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        private const string BaseAddress = "/groups";
        private readonly HttpClient _httpClient;

        public GroupsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IReadOnlyCollection<GroupModel>>> GetAllAsync(CancellationToken ct)
        {
            // TODO: handle possible _httpClient errors
            var response = await _httpClient.GetAsync(BaseAddress, ct);
            var result = await response.Content.ReadAsAsync<IReadOnlyCollection<GroupModel>>(ct);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<GroupModel>> GetByIdAsync(long id, CancellationToken ct)
        {
            // TODO: handle possible _httpClient errors
            var response = await _httpClient.GetAsync($"{BaseAddress}/{id}", ct);
            if(response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            var result = await response.Content.ReadAsAsync<GroupModel>(ct);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<GroupModel>> UpdateAsync(long id, GroupModel model, CancellationToken ct)
        {
            // TODO: handle possible _httpClient errors
            var response = await _httpClient.PutAsJsonAsync($"{BaseAddress}/{id}", model, ct);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            var result = await response.Content.ReadAsAsync<GroupModel>(ct);
            return Ok(result);
        }

        [HttpPut]
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<GroupModel>> AddAsync(GroupModel model, CancellationToken ct)
        {
            // TODO: handle possible _httpClient errors
            var response = await _httpClient.PostAsJsonAsync(BaseAddress, model, ct);
            var result = await response.Content.ReadAsAsync<GroupModel>(ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> RemoveAsync(long id, CancellationToken ct)
        {
            // TODO: handle possible _httpClient errors
            await _httpClient.DeleteAsync($"{BaseAddress}/{id}", ct);
            return NoContent();
        }
    }
}