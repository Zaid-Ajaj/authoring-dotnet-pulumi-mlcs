using Pulumi.Experimental.Provider;

var provider = 
    SimpleProvider
        .Create("test")
        .RegisterComponent<TestComponentArgs, TestComponent>("test:index:Test", 
            (request, args) => new TestComponent(request.Name, args))
        .Build();

await provider.Serve(args);