using System.Text.Json;
using ApiContracts.CommentDtos;
using ApiContracts.PostDtos;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Components.Services;

public class HttpCommentService : ICommentService
{
    
    private readonly HttpClient client;

    public HttpCommentService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<ActionResult<CommentDto>> UpdateComment(int id, UpdateCommentReq request)
    {
        HttpResponseMessage httpResponse = await client.PutAsJsonAsync($"comments/{id}", request); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<ActionResult<CommentDto>> GetSingleComment(int id)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"comments/{id}"); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } return JsonSerializer.Deserialize<CommentDto>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<IEnumerable<CommentDto>> GetMany(int? postId, int? userId)
    {
        HttpResponseMessage httpResponse = await client.GetAsync($"comments"); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        }
        return JsonSerializer.Deserialize<IEnumerable<CommentDto>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }

    public async Task<ActionResult> DeleteComment(int id)
    {
        HttpResponseMessage httpResponse = await client.DeleteAsync($"comments/{id}"); 
        string response = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new Exception(response);
        } return JsonSerializer.Deserialize<ActionResult>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
    }
}