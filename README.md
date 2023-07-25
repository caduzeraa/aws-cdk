# aws-cdk
 
 Notes
- CDK book to catch up
- Converts code into cloudformation and then deploys it to aws via stack
- Node JS based

Layers
- App: no cloudformtation equivalent
    - Stack: Cloudformation stack
        - Nested stack: cloudformation nested stack
        - Contruct: 1 or more cloudformation resources
- Can have muiltiple apps
- Apps can have multiple stacks

Where to check for constructs (modules)
- https://cdkpatterns.com/
- https://docs.aws.amazon.com/solutions/latest/constructs/welcome.html

How to build and deploy
This will synthesize the code and generate the unique IDs for the resources
'''
cdk synth
cdk bootstrap
cdk deploy
'''