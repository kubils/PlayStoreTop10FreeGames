# Play Store Top 10 Games .Net Core Rest API

[Top Free Games Url](https://play.google.com/store/apps/collection/cluster?clp=0g4cChoKFHRvcHNlbGxpbmdfZnJlZV9HQU1FEAcYAw%3D%3D:S:ANO1ljJ_Y5U&gsr=Ch_SDhwKGgoUdG9wc2VsbGluZ19mcmVlX0dBTUUQBxgD:S:ANO1ljL4b8c)

![image](https://user-images.githubusercontent.com/28518987/142706844-f660d982-b4d0-4bb4-8b76-9a74064c4193.png)

### Summary
Get Top 10 free games from google play store and get detailed information about games.
With background job, every 1 hour top 10 free game details save to database. Games some informations are mutable and they are holding datetime to add to the collection 
API has user register and user login authenticaon with JWT. With JWT token reach to API endpoins which top ten games and getting data for given TrackId. Given TrackId is returning the latest available data in mongodb top ten games collections. 

 Project deployed in Docker Compose, images: 
- .NET Core 5 API
- MongoDB
- MongoClient


```sh
- Build or rebuild services, Dockerfile
$ docker-compose build 
    - From  project directory, start up  application by running
$ docker-compose up
```

![image](https://user-images.githubusercontent.com/28518987/142706886-8f98a576-488f-4785-9d75-c2146948ebff.png)

### System Design, Tech and libraries
- .NET Core 5 Rest API
- MongoDB
- Hangfire 
- JWT, Swagger
- Onion Architecture, CQRS, MediatR, Automapper, HtmlAgilityPack
- XUnit, Moq

[Top Free Games Url](https://play.google.com/store/apps/collection/cluster?clp=0g4cChoKFHRvcHNlbGxpbmdfZnJlZV9HQU1FEAcYAw%3D%3D:S:ANO1ljJ_Y5U&gsr=Ch_SDhwKGgoUdG9wc2VsbGluZ19mcmVlX0dBTUUQBxgD:S:ANO1ljL4b8c)
 For storing Top 10 Free Games information in db happens with scheduled background job (Hangfire) every 1 hour, recurring job calls first html scraper operations then set the game details in the fields and add games to the list of games. List send repository for storing in collection.


- To access the links of the games,  Html scraper for reach game links with div class name and ahref gives us the links route of the games like "/store/apps/details?id=com.pubg.newstate" then games playstore full path  add to list.
    https://play.google.com/store/apps/details?id=com.pubg.newstate

-  From the games page is to get detail informations about the game and add to the list topGamesListDetail and the collection stores to mongodb.


## API
![image](https://user-images.githubusercontent.com/28518987/142706867-70fe90ba-2f00-4b4f-98b9-09a77e0094fe.png)

- For Authentication; user register and login for authenticate access get top game list or get game with TrackId endpoins, with login generated JWT token and swagger bearer authorize to apis.
- For register valid data is required like; valid email type and not exist in db, name and surname length 1-150, password at least 5 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character
![image](https://user-images.githubusercontent.com/28518987/142706919-f3a29777-362c-4bf7-982c-723add1077f4.png)
![image](https://user-images.githubusercontent.com/28518987/142706923-f1ba46fb-bbc4-433f-bcb7-7c9873ea9c1e.png)

- ![image](https://user-images.githubusercontent.com/28518987/142706978-df561577-1e87-40ec-813d-2e8741102c85.png)

- with login generates token
![image](https://user-images.githubusercontent.com/28518987/142706983-b52f7ac5-a3a6-48e0-91c7-cb57cc02f5b0.png)
 
- Not Authorize
![image](https://user-images.githubusercontent.com/28518987/142706989-e3c0c686-91d7-47a6-ab24-57b71c3e3976.png)

- Authorize
![image](https://user-images.githubusercontent.com/28518987/142707496-f069513c-bb7d-4e41-ba92-1e5f180e4e49.png)


![image](https://user-images.githubusercontent.com/28518987/142707027-258b4697-5140-4e08-a7f7-4e5a156f1ad5.png)
![image](https://user-images.githubusercontent.com/28518987/142707030-27bcda24-c7bd-4b8e-ac81-bdeb23ac724c.png)
![image](https://user-images.githubusercontent.com/28518987/142707021-cfd0bb80-42fd-4566-b8c6-48ac3ce97d3d.png)


### Hangfire

![image](https://user-images.githubusercontent.com/28518987/142706995-affd93e2-2914-4b16-bb36-f0406e986e55.png)

![image](https://user-images.githubusercontent.com/28518987/142707002-2d11654c-c8a0-4ead-a843-5a49eac8dbe2.png)

### MongoClient
![image](https://user-images.githubusercontent.com/28518987/142707008-52adaa03-ffe0-4853-aa93-fb9375639abe.png)
![image](https://user-images.githubusercontent.com/28518987/142707016-b48cb066-f2c4-427f-bf0a-eba41bc2afec.png)



