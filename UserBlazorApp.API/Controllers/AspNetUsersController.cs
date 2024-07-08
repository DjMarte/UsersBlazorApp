using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Dtos.Claims;
using UserBlazorApp.API.Dtos.Users;
using UserBlazorApp.API.Services;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AspNetUsersController(UserService userService) : ControllerBase
	{
		// GET: api/AspNetUsers
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserResponse>>> GetAspNetUsers() {
			var users = await userService.GetAllAsync();

			var userResponse = users.Select(user => new UserResponse {
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				PasswordHash = user.PasswordHash,
				PhoneNumber = user.PhoneNumber,
				LockoutEnd = user.LockoutEnd,
				Claims = user.AspNetUserClaims.Select(claim => new UserClaimResponse {
					Id = claim.Id,
					UserId = claim.UserId,
					ClaimType = claim.ClaimType,
					ClaimValue = claim.ClaimValue
				}).ToList(),
				Roles = user.Role.Select(role => new UserRoleResponse {
					Id = role.Id,
					Name = role.Name
				}).ToList()
			}).ToList();

			return Ok(userResponse);
		}

		// POST: api/AspNetUsers
		[HttpPost]
		public async Task<ActionResult<UserResponse>> PostAspNetUsers(UserRequest userRequest) {
			var user = new AspNetUsers {
				UserName = userRequest.UserName,
				Email = userRequest.Email,
				PasswordHash = userRequest.PasswordHash,
				PhoneNumber = userRequest.PhoneNumber,
				LockoutEnd = userRequest.LockoutEnd
			};

			var userCreated = await userService.AddAsync(user, userRequest.RoleIds);

			var userResponse = new UserResponse {
				Id = userCreated.Id,
				UserName = userCreated.UserName,
				Email = userCreated.Email,
				PasswordHash = userCreated.PasswordHash,
				PhoneNumber = userCreated.PhoneNumber,
				LockoutEnd = userCreated.LockoutEnd,
				Claims = new List<UserClaimResponse>(),
				Roles = userCreated.Role.Select(role => new UserRoleResponse {
					Id = role.Id,
					Name = role.Name
				}).ToList()
			};

			return CreatedAtAction("GetAspNetUsers", new { id = userResponse.Id }, userResponse);
		}

		// GET: api/AspNetUsers/5
		[HttpGet("{id}")]
		public async Task<ActionResult<UserResponse>> GetAspNetUsers(int id) {
			var user = await userService.GetIdAsync(id);

			if (user == null)
				return NotFound();

			var userResponse = new UserResponse {
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				LockoutEnd = user.LockoutEnd
			};

			return Ok(userResponse);
		}

		// PUT: api/AspNetUsers/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutAspNetUsers(int id, UserRequest userRequest) {
			

			var user = await userService.GetIdAsync(id);
			if (user == null)
				return NotFound();

			user.UserName = userRequest.UserName;
			user.Email = userRequest.Email;
			user.PasswordHash = userRequest.PasswordHash;
			user.PhoneNumber = userRequest.PhoneNumber;
			user.LockoutEnd = userRequest.LockoutEnd;

			await userService.UpdateAsync(user);

			return NoContent();
		}

		// DELETE: api/AspNetUsers/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAspNetUsers(int id) {
			var user = await userService.GetIdAsync(id);

			if (user == null) {
				return NotFound();
			}
			
			await userService.DeleteAsync(id);

			return NoContent();
		}
	}
}