using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Dtos.Claims;
using UserBlazorApp.API.Dtos.Roles;
using UserBlazorApp.API.Services;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetRolesController(IApiService<AspNetRoles> roleService) : ControllerBase
    {
        // GET: api/AspNetRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetAspNetRoles()
        {
			var roles = await roleService.GetAllAsync();

			var roleResponses = roles.Select(role => new RoleResponse {
				Id = role.Id,
				Name = role.Name,
				RoleClaims = role.AspNetRoleClaims.Select(claim => new RoleClaimResponse {
					Id = claim.Id,
					RoleId = claim.RoleId,
					ClaimType = claim.ClaimType,
					ClaimValue = claim.ClaimValue
				}).ToList()
			}).ToList();

			return Ok(roleResponses);
		}

		// POST: api/AspNetRoles
		[HttpPost]
		public async Task<ActionResult<RoleResponse>> PostAspNetRoles(RoleRequest roleRequest) {
			var role = new AspNetRoles {
				Name = roleRequest.Name,
				AspNetRoleClaims = roleRequest.RoleClaims.Select(claim => new AspNetRoleClaims {
					ClaimType = claim.ClaimType,
					ClaimValue = claim.ClaimValue
				}).ToList()
			};

			var roleCreated = await roleService.AddAsync(role);

			var roleResponse = new RoleResponse {
				Id = roleCreated.Id,
				Name = roleCreated.Name,
				RoleClaims = roleCreated.AspNetRoleClaims.Select(claim => new RoleClaimResponse {
					Id = claim.Id,
					RoleId = claim.RoleId,
					ClaimType = claim.ClaimType,
					ClaimValue = claim.ClaimValue
				}).ToList()
			};

			return CreatedAtAction("GetAspNetRoles", new { id = roleResponse.Id }, roleResponse);
		}

        // GET: api/AspNetRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoleResponse>> GetAspNetRoles(int id) {
			var role = await roleService.GetIdAsync(id);

			if (role == null) {
				return NotFound();
			}

			var roleResponse = new RoleResponse {
				Id = role.Id,
				Name = role.Name,
				RoleClaims = role.AspNetRoleClaims.Select(claim => new RoleClaimResponse {
					Id = claim.Id,
					RoleId = claim.RoleId,
					ClaimType = claim.ClaimType,
					ClaimValue = claim.ClaimValue
				}).ToList()
			};

			return Ok(roleResponse);
		}

        // PUT: api/AspNetRoles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspNetRoles(int id, RoleRequest roleRequest) {
			var role = await roleService.GetIdAsync(id);

			if (role == null) {
				return NotFound();
			}

			role.Name = roleRequest.Name;
			role.AspNetRoleClaims = roleRequest.RoleClaims.Select(claim => new AspNetRoleClaims {
				ClaimType = claim.ClaimType,
				ClaimValue = claim.ClaimValue,
				RoleId = id
			}).ToList();

			await roleService.UpdateAsync(role);

			return NoContent();
		}

        // DELETE: api/AspNetRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspNetRoles(int id) {
			var role = await roleService.GetIdAsync(id);

			if (role == null) {
				return NotFound();
			}

			await roleService.DeleteAsync(id);

			return NoContent();
		}
    }
}
