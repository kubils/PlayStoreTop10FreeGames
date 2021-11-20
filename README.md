# Play Store Top 10 Games .Net Core Rest API

[Top Free Games Url](https://play.google.com/store/apps/collection/cluster?clp=0g4cChoKFHRvcHNlbGxpbmdfZnJlZV9HQU1FEAcYAw%3D%3D:S:ANO1ljJ_Y5U&gsr=Ch_SDhwKGgoUdG9wc2VsbGluZ19mcmVlX0dBTUUQBxgD:S:ANO1ljL4b8c)
#resim1

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
###resim2

- For Authentication; user register and login for authenticate access get top game list or get game with TrackId endpoins, with login generated JWT token and swagger bearer authorize to apis.
- For register valid data is required like; valid email type and not exist in db, name and surname length 1-150, password at least 5 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character
###resim3 5
4
- with login generates token
5 
