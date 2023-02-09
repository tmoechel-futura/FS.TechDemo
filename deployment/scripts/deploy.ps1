Set-Location -Path "C:\Entwicklung\Learning\FS.TechDemo"
docker build . -t futuratechdemoacr.azurecr.io/order-service:1.2 -f FS.TechDemo.OrderService/Dockerfile
docker build . -t futuratechdemoacr.azurecr.io/delivery-service:1.2 -f FS.TechDemo.DeliveryService/Dockerfile
docker build . -t futuratechdemoacr.azurecr.io/buyer-bff:1.2 -f FS.TechDemo.BuyerBFF/Dockerfile

docker push futuratechdemoacr.azurecr.io/order-service:1.2
docker push futuratechdemoacr.azurecr.io/delivery-service:1.2
docker push futuratechdemoacr.azurecr.io/buyer-bff:1.2

