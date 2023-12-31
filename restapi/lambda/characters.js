exports.get = async function(event) {
    console.log("request:", JSON.stringify(event, undefined, 2));
    
    if (event.pathParameters && event.pathParameters['character']) {
        console.log("Received name: " + event.pathParameters.character);
        return {
            statusCode: 200,
            headers: { "Content-Type": "text/plain" },
            body: `You've asked for this character ${event.pathParameters.character}!\n`
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
            body: `New characted added: \n${body.name}`
        };
    }

    return {
        statusCode: 401,
        headers: { "Content-Type": "text/plain" },
        body: `Error! No character were provided on the body\n`
    };
};