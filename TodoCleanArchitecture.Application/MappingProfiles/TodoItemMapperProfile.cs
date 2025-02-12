using AutoMapper;
using TodoCleanArchitecture.Application.Features.Todos.Queries.Get;
using TodoCleanArchitecture.Application.Features.Todos.Queries.GetAll;
using TodoCleanArchitecture.Domain.Features.Todos;

namespace TodoCleanArchitecture.Application.MappingProfiles;

public class TodoItemMapperProfile : Profile
{
    public TodoItemMapperProfile()
    {
        CreateMap<TodoItem, GetQueryResult>().ReverseMap();
        CreateMap<TodoItem, GetAllQueryResult>().ReverseMap();
    }
}
