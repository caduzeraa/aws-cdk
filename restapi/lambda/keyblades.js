exports.get = async function(event) {
    console.log("request:", JSON.stringify(event, undefined, 2));
    
    if (event.pathParameters && event.pathParameters['keyblade']) {
        console.log("Received name: " + event.pathParameters.keyblade);
        return {
            statusCode: 200,
            headers: { "Content-Type": "text/plain" },
            body: `You've asked for this keyblade ${event.pathParameters.keyblade}!\n`
        };
    }
    return {
        statusCode: 200,
        headers: { "Content-Type": "text/plain" },
        body: `Hello, This is the ${event.path} endpoint!\n`
    };
};

exports.post = async function(event) {
    console.log("request:", JSON.stringify(event, undefined, 2));
    
    if (event.body !== null && event.body !== undefined)
    {
        var body = JSON.parse(event.body)
        
        return {
            statusCode: 200,
            headers: { "Content-Type": "text/plain" },
            body: `New keyblade added: \n${body.name}`
        };
    }

    return {
        statusCode: 401,
        headers: { "Content-Type": "text/plain" },
        body: `Error! No keyblade were provided on the body\n`
    };
};