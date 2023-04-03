// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

await using var ctx = new BloggingContext();
await ctx.Database.MigrateAsync();

 ctx.Add(new Blog()
 {
     Name = $"{Guid.NewGuid():N} - blog",
     Posts = new List<Post>()
     {
         new()
         {
             Title = $"{Guid.NewGuid():N} - post - 1",
             Content = "Lorem ipsum ergo sum"
         },
         new()
         {
             Title = $"{Guid.NewGuid():N} - post - 2",
             Content = "Lorem ipsum ergo sum"
         },

     }
 });
 await ctx.SaveChangesAsync();
 
 var items = ctx.Posts.Select(post => new {post.Id, post.Title, post.BlogId});

foreach (var item in items)
{
    Console.WriteLine($"{item.BlogId} - {item.Id} - {item.Title}");
}