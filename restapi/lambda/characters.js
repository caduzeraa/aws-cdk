const { Client } = require('pg');

const client = new Client({
    user: process.env.DB_USER,
    host: process.env.DB_HOST,
    database: process.env.DB_DATABASE,
    password: process.env.DB_SECRET,
    port: 5432
});

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

        try {

            await client.connect();
            callback(null, "Connected Successfully");

            const queryString = {
                text: "INSERT INTO characters VALUES ($1, $2, $3)",
                values: [body.name, body.age, body.description],
            };
            const result = await client.query(queryString);
            //your code here
    
        } catch (err) {
    
            callback(null, "Failed to Connect Successfully");
            throw err;
            //error message
        }
    
        client.end();
        
        return {
            statusCode: 200,
            headers: { "Content-Type": "text/plain" },
            body: `New characted added: ${character.name}!\n`
        };
    }

    return {
        statusCode: 401,
        headers: { "Content-Type": "text/plain" },
        body: `Error!\n`
    };
};