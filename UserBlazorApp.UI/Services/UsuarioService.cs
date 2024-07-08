using System.Net.Http.Json;
using UsersBlazorApp.Data.Models;

namespace UserBlazorApp.UI.Services;

public class UsuarioService(HttpClient httpClient)
{
	public async Task<List<AspNetUsers>> GetAllAsync() {
		var resultado = await httpClient.GetAsync("api/AspNetUsers");
		if (resultado.IsSuccessStatusCode)
			return await resultado.Content.ReadFromJsonAsync<List<AspNetUsers>>();

		return null;
	}

	public async Task<AspNetUsers> GetByIdAsync(int id) {
		var resultado = (await httpClient.GetAsync($"api/AspNetUsers/{id}"))!;

		if (resultado.IsSuccessStatusCode)
			return await resultado.Content.ReadFromJsonAsync<AspNetUsers>();

		return null!;
	}

	public async Task<AspNetUsers> CreateAsync(AspNetUsers usuario) {
		var resultado = await httpClient.PostAsJsonAsync("api/AspNetUsers", usuario);

		if (resultado.IsSuccessStatusCode)
			return await resultado.Content.ReadFromJsonAsync<AspNetUsers>();

		return null;
	}

	public async Task<bool> UpdateAsync(AspNetUsers usuario) {
		var resultado = await httpClient.PutAsJsonAsync($"api/AspNetUsers/{usuario.Id}", usuario);
		return resultado.IsSuccessStatusCode;
	}

	public async Task<bool> DeleteAsync(int id) {
		var resultado = await httpClient.DeleteAsync($"api/AspNetUsers/{id}");
		return resultado.IsSuccessStatusCode;
	}
}