
# USocketNet

#### Short Description: Self-Host Realtime Bidirectional Event-Based Communication for your Game or Chat Application. 

The USocketNet is currently designed and developed for Unity Engine. It is a multi-platform by design that can be used through mobile, computers, or even web. We advised the developers to report any issues or bugs immediately to further fix and improve our code. We are driven to add new features that will make this project awesome for everyone.

### Its main features are:

- Realtime WebSocket connection using the stable and reliable socket.io-engine.
- Host your own server anywhere, it can be any VPS, CLOUD, or NodeJS hosting.
- Cross-Platform with Unity, if unity supports it, we will also support it.
- Yes! We support WebGL build even if threading is not allowed on the browser.
- Reconnect event handling which automatically resyncs client to the server.
- Dedicated Realtime GUI backend page for all server instance.
- Stability (socket.io), Security (NodeJS) and Scalability (NGINX).
- Matchmaking mechanism for auto, create, join and lots of options.
- Dedicate and easy to use, we have messaging service for private and public.
- Lots of features to be announce! Stay tuned for more updates.

* Connections are established even in the presence of:
  - proxies and load balancers with Nginx Server.
  - personal firewall and antivirus software by Socket.IO.
  - in memory json data cache with Redis Server.
  - multi instancing and keymetrics by npm PM2.
  - easy npm devDependencies updates NPM npm-gui.

**Note:** USocketNet is not a WebSocket implementation. Although USocketNet indeed uses WebSocket as a transport when possible, it adds some metadata to each packet: the packet type, the namespace and the ack id when a message acknowledgement is needed. That is why a WebSocket client will not be able to successfully connect to a USocketNet server, and a USocketNet client will not be able to connect to a WebSocket server (like `ws://echo.websocket.org`) either.

## Built With

This project was made possible by NodeJS / OpenJS developers. Thank you for making server side development easy.

* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json/) - JSON Serialization
* [System.Collection](https://dotnet.microsoft.com/) - .Net Collections
* [Websocket.client](https://github.com/Marfusios/websocket-client) - Websocket .Net Implementation

## Contributing

Please read [CONTRIBUTING](CONTRIBUTING) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/BytesCrafter). 

## Authors

* **Bytes Crafter** - [Website](https://www.bytescrafter.net) - [Github](https://github.com/BytesCrafter/USocketNet-on-NodeJS)

## License

This project is licensed under the GNU GPL License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* NodeJS - OpenJS Developer
* WordPress.Org
* StackOverflow :D
