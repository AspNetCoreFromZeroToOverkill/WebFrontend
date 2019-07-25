# builder
FROM node:lts-alpine as builder
WORKDIR /app

## Storing node modules on a separate layer will prevent unnecessary npm installs at each build
COPY package*.json ./
RUN npm install

COPY . .
RUN npm run build

# Build runtime image
FROM nginx:alpine as runtime

## Copy our default nginx config
COPY nginx.conf /etc/nginx/conf.d/default.conf

## Remove default nginx website
RUN rm -rf /usr/share/nginx/html/*

## From ‘builder’ stage copy over the artifacts in dist folder to default nginx public folder
COPY --from=builder /app/dist /usr/share/nginx/html

# Sample build command
# docker build -t codingmilitia/webfrontend/spa .