# gRPC Web Client

## Install Protoc
* Download Protoc.exe https://github.com/google/protobuf/releases (protoc-3.17.3-win64.zip)
* Download protoc-gen-grpc-web.exe https://github.com/grpc/grpc-web/releases (protoc-gen-grpc-web-1.2.1-windows-x86_64.exe) (Rename to protoc-gen-grpc-web.exe)
* Add to environment variable Path
* Start from command line protoc

## Generate gRPC javascript files
```batch
protoc --proto_path="../GrpcService/Protos/" greet.proto --js_out=import_style=commonjs:. --grpc-web_out=import_style=commonjs,mode=grpcwebtext:.
```

## Build
```batch
npx webpack main.js
```

Copy Web/dist/main.js to GrpcService/wwwroot/main.js