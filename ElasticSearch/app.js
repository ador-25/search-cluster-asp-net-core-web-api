const { Client } = require("@elastic/elasticsearch");
const config = require("config");
const elasticConfig = config.get("elastic");
const https = require("https");
const axios = require("axios");
const client = new Client({
  cloud: {
    id: elasticConfig.cloudID,
  },
  auth: {
    username: elasticConfig.username,
    password: elasticConfig.password,
  },
});

// Express server
const express = require("express");
const cors = require("cors");
const app = express();

app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

async function read(clusterId, keyword) {
  const { body } = await client.search({
    index: clusterId.toLowerCase(),
    body: {
      query: {
        match: { data: keyword },
      },
    },
  });

  return body.hits.hits;
}

// Search route
app.get("/search/:clusterId", async (req, res) => {
  try {
    const { clusterId } = req.params;
    const { keyword } = req.query;

    if (!keyword)
      return res.status(400).json({ msg: "Please give me keyword" });

    const result = await read(clusterId, keyword);
    return res.status(200).json(result);
  } catch (err) {
    console.log(err);
    return res.status(500).json({ msg: "Internal server error!", err });
  }
});

// https://localhost:44398/api/searchcluster/get/02C1D3B9-B98F-4A58-ECA3-08D9CA8FBA6C

async function forEachAsync(arr, callback) {
  for (let index = 0; index < arr.length; index++) {
    await callback(arr[index]);
  }
}

const noSSL = axios.create({
  baseURL: "https://localhost:44398",
  httpsAgent: new https.Agent({
    rejectUnauthorized: false,
  }),
});

// Indexing code below
const indexName = "02C1D3B9-B98F-4A58-ECA3-08D9CA8FBA6C";
let parsedData = [];

const searchCluster = async (clusterId) => {
  try {
    const res = await noSSL.get(`/api/searchcluster/get/${clusterId}`);
    console.log(res.status);
    parsedData = res.data;
  } catch (err) {
    console.log(err);
  }
};

const runIndex = async () => {
  await forEachAsync(parsedData, async (data) => {
    try {
      await client.index({
        index: indexName.toLowerCase(),
        body: {
          urlId: data.urlId,
          data: data.data,
        },
      });
      console.log(`Success: ${data.urlId}`);
    } catch (err) {
      console.log(`Failure: ${data.urlId}`);
      console.log(err);
    }
  });
};

// (async () => {
//   try {
//     await searchCluster(indexName);
//     console.log(parsedData.length);
//     await runIndex();
//   } catch (err) {
//     console.log(err)
//   }
// })()

const PORT = 4000;
app.listen(PORT, () => console.log(`Server running at port ${PORT}`));
