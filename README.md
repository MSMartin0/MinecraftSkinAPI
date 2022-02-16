# MinecraftSkinAPI
An API for fetching user skins and certain components of the skin

# Security notice

This API does not use HTTPS, so you will need a third party provider to enable SSL communication

# Setup
## Docker

### Build
`docker build -t minecraftskinapi .`

Alternatively, run the following command to pull my latest image
`sudo docker run msmartin2001/minecraftskinapi`

### Running
`docker run -p {port}:80 minecraftskinapi`
{port} is the port you want your service running on

#Endpoints
Go to `http://localhost:{port}/swagger/index.html` to see the API endpoints and their specifications