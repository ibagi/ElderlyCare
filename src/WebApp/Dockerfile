FROM node:alpine as build

WORKDIR /app

ENV VITE_API_URL=http://localhost:5010
ENV VITE_ROBOT_WORKER_URL=http://localhost:5020

COPY ./WebApp/ /app/

RUN npm ci
RUN npm run build

FROM nginx
COPY --from=build /app/dist /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]