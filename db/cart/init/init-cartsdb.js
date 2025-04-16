db = db.getSiblingDB("cartsdb");
db.createCollection("carts");
db.carts.createIndex({ "UserId": 1 });
