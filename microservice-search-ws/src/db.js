import mongoose from 'mongoose';

mongoose.set('strictQuery', false);

let isConnected = false;

export async function connectDB() {
  if (isConnected) return;

  const MONGO_HOST = process.env.MONGO_PRODUCT_HOST || 'mongo-product';
  const MONGO_PORT = process.env.MONGO_PRODUCT_PORT || '27017';
  const MONGO_DB   = process.env.MONGO_PRODUCT_DB   || 'productsdb';
  const uri = `mongodb://${MONGO_HOST}:${MONGO_PORT}/${MONGO_DB}`;
  console.log('Trying to connect to MongoDB:', uri);

  try {
    await mongoose.connect(uri, { 
      useNewUrlParser: true, 
      useUnifiedTopology: true 
    });
    isConnected = true;
    console.log('Connection to mongo-product successful.');
  } catch (err) {
    console.error('Error connecting to mongo-product:', err);
    throw err;
  }
}

const productSchema = new mongoose.Schema({
  nom_produit: String,
  description_courte: String,
  angle_principale: String,
});

export const Product = mongoose.model('Product', productSchema, 'products');
