using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data;
using API.Data.respositories;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace API.Controllers;
 
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {

        var users = await _repository.GetMembersAsync();
        
        return Ok(users);
    }
    
    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await _repository.GetMemberAsync(username);

        return Ok(user);
    }
}