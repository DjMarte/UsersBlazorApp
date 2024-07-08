using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Dtos.Claims;
using UserBlazorApp.API.Services;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserClaimsController(IApiService<AspNetUserClaims> userClaimsService) : ControllerBase
    {
        // GET: api/AspNetUserClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserClaimResponse>>> GetAspNetUserClaims()
        {
			var claims = await userClaimsService.GetAllAsync();
			var response = claims.Select(claim => new UserClaimResponse {
				Id = claim.Id,
				UserId = claim.UserId,
				ClaimType = claim.ClaimType,
				ClaimValue = claim.ClaimValue
			}).ToList();

			return Ok(response);
		}

		// POST: api/AspNetUserClaims
		[HttpPost]
		public async Task<ActionResult<UserClaimResponse>> PostAspNetUserClaims(UserClaimRequest userClaimRequest) {
			var claim = new AspNetUserClaims {
				UserId = userClaimRequest.UserId,
				ClaimType = userClaimRequest.ClaimType,
				ClaimValue = userClaimRequest.ClaimValue
			};

			var createdClaim = await userClaimsService.AddAsync(claim);
			var response = new UserClaimResponse {
				Id = createdClaim.Id,
				UserId = createdClaim.UserId,
				ClaimType = createdClaim.ClaimType,
				ClaimValue = createdClaim.ClaimValue
			};

			return CreatedAtAction("GetAspNetUserClaims", new { id = response.Id }, response);
		}

        // GET: api/AspNetUserClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserClaimResponse>> GetAspNetUserClaims(int id) {
			var claim = await userClaimsService.GetIdAsync(id);

			if (claim == null) {
				return NotFound();
			}

			var response = new UserClaimResponse {
				Id = claim.Id,
				UserId = claim.UserId,
				ClaimType = claim.ClaimType,
				ClaimValue = claim.ClaimValue
			};

			return Ok(response);
		}

        // PUT: api/AspNetUserClaims/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetUserClaims(int id, UserClaimRequest userClaimRequest) {
			var claim = await userClaimsService.GetIdAsync(id);
			if (claim == null) {
				return NotFound();
			}

			claim.UserId = userClaimRequest.UserId;
			claim.ClaimType = userClaimRequest.ClaimType;
			claim.ClaimValue = userClaimRequest.ClaimValue;

			await userClaimsService.UpdateAsync(claim);

			return NoContent();
		}

        // DELETE: api/AspNetUserClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetUserClaims(int id) {
			var claim = await userClaimsService.GetIdAsync(id);

			if (claim == null) {
				return NotFound();
			}

			await userClaimsService.DeleteAsync(id);

			return NoContent();
		}
    }
}
