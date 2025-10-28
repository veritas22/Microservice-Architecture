# Порядок для выполнения задания 
1) minikube start --driver=hyperv
2) minikube dashboard
3) postgresql в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/pghelm
- helm install pgrus -f values.yaml oci://registry-1.docker.io/bitnamicharts/postgresql
4) rabbit в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/rabbitmq
-  helm install -f values.yaml rabbitmq oci://registry-1.docker.io/bitnamicharts/rabbitmq
5) redis в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/redis
- helm install redis oci://registry-1.docker.io/bitnamicharts/redis -f values.yaml
6)  minikube addons enable ingress
7) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/apigetway
- helm install  apigetway .
8) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/order
- helm install  order .
9) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/notification
- helm install  notification .
10) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/billing
- helm install  billing .
11) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/userservice
- helm install  userservice .
12) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/warehouse
- helm install  warehouse .
13) в папке https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/Helm/delivery
- helm install  delivery .
14) Otlp для работы
https://github.com/veritas22/Portfolio/tree/main/OTUS_Microservice_Architecture/OtusFinal/OTLP-compose
- docker compose up
15) базы данных kubectl port-forward --namespace default svc/pgrus-postgresql 5438:5432
16)  Подменяем хост
-  minikube ip
-  C:\Windows\System32\drivers\etc\hosts
- пример 192.168.104.108 arch.homework
- пример 192.168.104.108 rabbit.rabbit.rabbit
