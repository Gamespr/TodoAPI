using AutoMapper;
using TodoApi.Dto;
using TodoApi.Models;

namespace TodoApi.Profiles
{
    public class TodoListProfile:Profile
    {
        public TodoListProfile()
        {
            CreateMap<TodoList, TodoListSelectDto>();

            //如果對應的(多個)欄位名稱不一樣的話可以用ForMember設定
            //CreateMap<TodoList, TodoListSelectDto>()
            //.ForMember(
            //a=>a.新的欄位名稱,
            //b=>b.MapFrom(c=>c.原本欄位的名稱)
            //)
            //.ForMember(...)
            //...;
        }
    }
}
