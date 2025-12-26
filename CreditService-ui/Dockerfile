
ARG NODE_VERSION=22.12.0

################################################################################
# Стадия 1: Сборка (Build)
FROM node:${NODE_VERSION}-alpine as build

WORKDIR /usr/src/app

# Используем кеш для npm
RUN --mount=type=bind,source=package.json,target=package.json \
    --mount=type=bind,source=package-lock.json,target=package-lock.json \
    --mount=type=cache,target=/root/.npm \
    npm ci

COPY . .

RUN npm run build

################################################################################
# Стадия 2: Финальный образ (Nginx)

FROM nginx:stable-alpine as final

COPY nginx.conf /etc/nginx/conf.d/default.conf

COPY --from=build /usr/src/app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]