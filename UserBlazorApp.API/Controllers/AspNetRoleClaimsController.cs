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
    public class AspNetRoleClaimsController(IApiService<AspNetRoleClaims> roleClaimsService) : ControllerBase
    {
        // GET: api/AspNetRoleClaims
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleClaimResponse>>> GetAspNetRoleClaims()
        {
			var claims = await roleClaimsService.GetAllAsync();
			var response = claims.Select(claim => new RoleClaimResponse {
				Id = claim.Id,
				RoleId = claim.RoleId,
				ClaimType = claim.ClaimType,
				ClaimValue = claim.ClaimValue
			}).ToList();

			return Ok(response);
		}

		// POST: api/AspNetRoleClaims
		[HttpPost]
		public async Task<ActionResult<RoleClaimResponse>> PostAspNetRoleClaims(RoleClaimRequest roleClaimRequest) {
			var claim = new AspNetRoleClaims {
				RoleId = roleClaimRequest.RoleId,
				ClaimType = roleClaimRequest.ClaimType,
				ClaimValue = roleClaimRequest.ClaimValue
			};

			var createdClaim = await roleClaimsService.AddAsync(claim);

			var response = new RoleClaimResponse {
				Id = createdClaim.Id,
				RoleId = createdClaim.RoleId,
				ClaimType = createdClaim.ClaimType,
				ClaimValue = createdClaim.ClaimValue
			};

			return CreatedAtAction("GetAspNetRoleClaims", new { id = response.Id }, response);
		}

        // GET: api/AspNetRoleClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleClaimResponse>> GetAspNetRoleClaims(int id) {
			var claim = await roleClaimsService.GetIdAsync(id);

			if (claim == null) {
				return NotFound();
			}

			var response = new RoleClaimResponse {
				Id = claim.Id,
				RoleId = claim.RoleId,
				ClaimType = claim.ClaimType,
				ClaimValue = claim.ClaimValue
			};

			return Ok(response);
		}

        // PUT: api/AspNetRoleClaims/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetRoleClaims(int id, RoleClaimRequest roleClaimRequest) {
			var claim = await roleClaimsService.GetIdAsync(id);

			if (claim == null) {
				return NotFound();
			}

			claim.RoleId = roleClaimRequest.RoleId;
			claim.ClaimType = roleClaimRequest.ClaimType;
			claim.ClaimValue = roleClaimRequest.ClaimValue;

			await roleClaimsService.UpdateAsync(claim);

			return NoContent();
		}

        // DELETE: api/AspNetRoleClaims/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetRoleClaims(int id) {
			var claim = await roleClaimsService.GetIdAsync(id);

			if (claim == null) {
				return NotFound();
			}

			await roleClaimsService.DeleteAsync(id);

			return NoContent();
		}
    }
}
