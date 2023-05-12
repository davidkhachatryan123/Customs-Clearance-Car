FROM node:18-alpine as build
WORKDIR /app

ARG api_endpoint="localhost"
ENV API_ENDPOINT=$api_endpoint

RUN npm install -g @angular/cli

COPY package.json .
COPY package-lock.json .

RUN npm install -g npm@9.6.4
RUN npm install

COPY . .
RUN apk add gettext
RUN envsubst < src/environments/environment.prod.ts.tpl > src/environments/environment.prod.ts
RUN npm run build -- --c production

FROM nginx as runtime
COPY --from=build /app/dist/customs-clearance-car /usr/share/nginx/html