#!/bin/sh

# https://spin.atomicobject.com/2017/08/24/start-stop-bash-background-process/
trap "exit" INT TERM ERR
trap "kill 0" EXIT

npm run serve --prefix ./client/ &

dotnet watch --project ./server/src/CodingMilitia.PlayBall.WebFrontend.BackForFront.Web run --environment Development --urls http://localhost:5000 &

wait