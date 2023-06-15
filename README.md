
## How to run

The command line interface can be run against example files already in the repository as follows

```
dotnet run --project .\ProductSearch.Console\ -- .\products-example.txt .\partners-example.txt YouTube 04-01-2012
```

If you're unsure of which argument order, you can run `dotnet run --project .\ProductSearch.Console\ -- --help`


## Running Tests

The tests can be run from the the project root with `dotnet test`


## Notes

I ran out of time (3hr limit) but here are some next steps
- There are still some validation loopholes. Primarily around parsing
- I'd add a test to enforce search results being sorted. I didn't notice that when I first looked through acceptance criteria
- I'd add another adapter to run the test suite against the CLI to ensure no integration issues
- I would have added more incremental tests, but given the timeline I opted to lean into the acceptance tests instead of isolating properties

