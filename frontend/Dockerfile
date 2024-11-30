FROM node:alpine
WORKDIR /usr/src/app
COPY . /usr/src/app
RUN npm install -g @angular/cli@18.2.8
RUN npm install
CMD ["ng", "serve", "--host", "0.0.0.0"]
