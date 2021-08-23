const {HelloRequest, HelloReply} = require('./greet_pb.js');
const {GreeterClient} = require('./greet_grpc_web_pb.js');

var echoService = new GreeterClient('https://localhost:5001');

var request = new HelloRequest();
request.setName('John (BrowserClient)');

echoService.sayHello(request, { }, function(err, response) {
  console.log("Response", response);
});


console.log("hello");