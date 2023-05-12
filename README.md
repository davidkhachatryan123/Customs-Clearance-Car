# Customs Clearance Car (C3)

This project was created to improve the skills of working with Angular and ASP.NET Core, and the main goal was to develop software for calculate customs clearance of car.

## Install Customs-Clearance-Car with docker

### Requirements

* Docker. Please, note that `docker-compose` is needed too and is included in
the Docker Desktop installation. Docker Desktop is available for
[Mac](https://docs.docker.com/desktop/install/mac-install/),
[Windows](https://docs.docker.com/desktop/install/windows-install/) and
[Linux](https://docs.docker.com/desktop/install/linux-install/).

### 1. Clone this repository and locate in terminal ***src*** folder

### 2. For application configuration open file named as ***.env*** in project directory

### 3. Change in this file ***API_ENDPOINT*** and ***SA_PASSWORD***:

```
API_ENDPOINT: "localhost" 	# ip address of host for api calls from client
SA_PASSWORD: "Q7YUZWH84Lg&"  	# password for MSSQL server
```

### 4. Run in project directory the following command:

```
docker-compose up -d
```

### 5. Go to follwing [link](http://localhost) in your browser:**

```
http://localhost
```

**Enjoy the Application 🎁🎉✨**