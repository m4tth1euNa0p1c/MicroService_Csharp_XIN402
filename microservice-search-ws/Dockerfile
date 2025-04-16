FROM node:18-alpine

ENV NODE_ENV=production
WORKDIR /app

COPY package*.json ./

RUN npm install --omit=dev
# ou npm ci --omit=dev si vous avez un package-lock.json strictement aligné

COPY . .

EXPOSE 3003
CMD ["node", "src/index.js"]
