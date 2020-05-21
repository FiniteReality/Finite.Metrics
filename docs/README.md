# Finite.Metrics #

A metrics library for .NET, written in the style of
Microsoft.Extensions.Logging.

## Why? ##

I believe recording metrics should be simple and easy. No need for complicated
configuration of individual options like units, reservoirs, histograms, et
cetera. Those should all be left to the system which processes and displays the
metrics.

Originally, I was looking at [AppMetrics] as I didn't want to write a custom
metrics solution for a project I was working on. However, its approach to
metrics left me disillusioned, and my trusty $searchEngine-Fu wasn't revealing
other options, so I decided that I unfortunately had to implement my own.

At first, I was looking to implement the metrics solution as part of my normal
logging, so that the "standard interfaces" would also track metrics, and
additionally giving me the ability to easily add metrics to systems which did
not normally have them with little effort. (Systems like IdentityServer4,
Kestrel, and Entity Framework, where adding metrics is more of a tedious matter
of finding the correct interfaces and plugging in the correct implementations.)

However, this quickly turned into a mess, as I would have to manually map which
log messages were metrics, and what properties to track, so on so forth. As
such, I decided to start a new abstraction centered around a simple
`Log<T, TTags>(T value, TTags tags = null)` interface, which could be used to
log any time-series value; even structured data, if your provider supported it.

[AppMetrics]: https://www.app-metrics.io/
