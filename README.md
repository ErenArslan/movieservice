# MovieService
DDD, Clean Architecture, Microservices

## Technology Sets
Cache = Redis
Log = ELK (Elasticsearch)
Database = Mongodb
MQ = RabbitMQ

## Installation

just need docker to use

```bash
docker-compose up
```

## Usage

run on http 8000 port. You can use swagger. http://localhost:8000/swagger


You need Token : eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Inh6MnlzU2plc28zTENacG5RTUJaZSJ9.eyJpc3MiOiJodHRwczovL2Rldi1wNGp6NXU1Yy51cy5hdXRoMC5jb20vIiwic3ViIjoidDI4eU94OUpPcEVibndNZGhKT01GRjFZNjJVOWNKN0dAY2xpZW50cyIsImF1ZCI6Ik1vdmllU2VydmljZSIsImlhdCI6MTU5ODE3NzA2NiwiZXhwIjoxNTk4MjYzNDY2LCJhenAiOiJ0Mjh5T3g5Sk9wRWJud01kaEpPTUZGMVk2MlU5Y0o3RyIsImd0eSI6ImNsaWVudC1jcmVkZW50aWFscyJ9.Ue00Hw8Bl9hQ6RpcsL9MA5sAmAYkw8gZhKjnR7yGzTOvWtJPug7zi1NWgsFxH_ew-Oy-tRd47IH0ObOAqLA_aaeBHe50429vUZVLv6g-DKwpyRE08tqpoIa1WDD4YrjpQeE4nbD3R0yd1QWCXYsUXFpYfrJr2U5i1mG-FUwkATcx4vrIbGKQCnhNGrwv1-avi24X-WHCWGGmc-HfqvOnqodH1ziaqYXMBQFCvmpjiSQurJ4SLkIHT4xCRldEGi29FCERcb5pH-FvuSMZ0PvLoARxycbKlanwyhywiNqmm4aYk4hbCulVFA6tSOqQ41ldT3vH2CkI8Xa3iK_vIW0mXg



## MovieService API

Request/Response Api

## Movie Provider Service

Fetch movies from external API

## Notification Service

sends email for recommended movies
