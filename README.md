# SuperAgent.NET
[SuperAgent](https://github.com/visionmedia/superagent) in .NET

Request with less suck!


## Get Started
```csharp
using SuperAgent;

...
	
	var res = Request
				.Get("http://www.baidu.com")
				.Query("name1","value1")
				.Query(new {
					name2 = value2,
					name3 = value3
				})
				.End();
	res.Text ...
```

## TODO
- add more method on `Request`
- add more method/prop on `Response`
- add `async/await` TPL based asynchronous style support,that works on .NET 4.5
- add more test
- add Nuget Package
- add doc

这几天不太舒服,写不下去了,先这样吧...


## License
MIT