import express from 'express';
import http from 'http';
import { WebSocketServer } from 'ws';
import { connectDB, Product } from './db.js';

const app = express();

app.use(express.json());

app.get('/ping', (req, res) => {
  res.json({ message: 'pong from microservice-search-ws', time: Date.now() });
});

app.get('/search', async (req, res) => {
  try {
    const { q = '', cat = '' } = req.query;
    console.log('HTTP search endpoint called with:', { q, cat });

    if (q.length < 3) {
      return res.status(400).json({ error: 'La requête doit contenir au moins 3 caractères.' });
    }

    const query = {};
    if (cat) {
      query.categorie = new RegExp(`^${cat}$`, 'i');
    }
    query.$or = [
      { nom_produit: new RegExp(q, 'i') },
      { description_courte: new RegExp(q, 'i') }
    ];

    const results = await Product.find(query).limit(10).exec();
    res.json({ type: 'results', data: results });
  } catch (err) {
    console.error('Error handling HTTP /search:', err);
    res.status(500).json({ error: 'Erreur interne du serveur' });
  }
});

const server = http.createServer(app);

const wss = new WebSocketServer({ server });

wss.on('connection', (socket) => {
  console.log('New WebSocket client connected.');

  socket.on('message', async (raw) => {
    try {
      const { q = '', cat = '' } = JSON.parse(raw.toString());
      console.log('WS search request:', { q, cat });

      if (q.length < 3) {
        socket.send(JSON.stringify({ type: 'results', data: [] }));
        return;
      }

      const query = {};
      if (cat) {
        query.categorie = new RegExp(`^${cat}$`, 'i');
      }
      query.$or = [
        { nomProduit: new RegExp(q, 'i') },
        { descriptionCourte: new RegExp(q, 'i') }
      ];

      const results = await Product.find(query).limit(10).exec();
      socket.send(JSON.stringify({ type: 'results', data: results }));
    } catch (err) {
      console.error('Error handling WS message:', err);
      socket.send(JSON.stringify({ type: 'error', message: err.message }));
    }
  });

  socket.on('close', () => {
    console.log('WebSocket client disconnected.');
  });
});

(async () => {
  try {
    await connectDB();
  } catch (err) {
    console.error("Fatal: couldn't connect to mongo-product:", err);
    process.exit(1);
  }
})();

const PORT = process.env.PORT || 3003;
server.listen(PORT, () => {
  console.log(`microservice-search-ws listening on port ${PORT}`);
});
