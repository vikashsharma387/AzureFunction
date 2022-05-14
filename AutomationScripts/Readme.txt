I am using node.js applciation to trigger the request and do the prformance testing
To invoke the request in parallel . we need to allocate high heap memory explicitly to the node.js application
command to allocate and run the node.js application.

node --max-old-space-size=24576 .\ConfigureParallelRequestProcessing.js