Set-Location -Path "C:\Projects\TechDemo"
docker build . -t akupe/order-service:1.2 -f FS.TechDemo.OrderService/Dockerfile
docker build . -t akupe/delivery-service:1.2 -f FS.TechDemo.DeliveryService/Dockerfile
docker build . -t akupe/buyer-bff:1.2 -f FS.TechDemo.BuyerBFF/Dockerfile

docker image push akupe/order-service:1.2
docker image push akupe/delivery-service:1.2
docker image push akupe/buyer-bff:1.2
