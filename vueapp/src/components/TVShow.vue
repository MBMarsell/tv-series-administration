<template>
  <div>
    <h1>TV Shows</h1>
    <form @submit.prevent="handleFileUpload">
      <input type="file" ref="fileInput" accept=".txt" />
      <button type="submit">Upload config</button>
    </form>
    <ul class="tv-show-list" v-if="uploadedConfig && tvShows.length">
      <li v-for="tvShow in tvShows" :key="tvShow.name" class="tv-show-item">
        <div class="tv-show">
          <div class="tv-show-image">
            <img :src="tvShow.image?.medium" :alt="tvShow?.name" />
          </div>
        </div>
        <div class="tv-show-info">
          <h2>{{ tvShow?.name }}</h2>
          <!-- <p>{{ tvShow.summary }}</p> -->
          <p>Rating: {{ tvShow.rating?.average }}</p>
          <p>
            Genre:
            {{
              tvShow.genres && tvShow?.genres.length > 0 ? tvShow.genres[0] : ''
            }}
          </p>
          <p>Network: {{ tvShow.network?.name }}</p>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      tvShows: [],
      uploadedConfig: false,
    };
  },
  methods: {
    handleFileUpload() {
      const file = this.$refs.fileInput.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        const tvShowTitles = reader.result.split('\n');
        tvShowTitles.forEach((title) => {
          this.getTVShowData(title.trim());
          //   console.log(title.trim());
        });
      };
      reader.readAsText(file);
    },
    async getTVShowData(title) {
      const res = await axios.get('https://localhost:7150/api/TVShows/Show/', {
        params: {
          showTitle: title,
        },
      });
      const tvShowData = res.data;
      this.tvShows.push(tvShowData);
      this.uploadedConfig = true;
      await new Promise((resolve) => setTimeout(resolve, 2000));
      //   console.log(tvShowData);
    },
  },
};
</script>

<style scoped>
.tv-shows {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 20px;
}

.tv-show-list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 20px;
  justify-items: center;
}

.tv-show-item {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: center;
  gap: 20px;
  padding: 20px;
  background-color: #f5f5f5;
  border-radius: 5px;
}

.tv-show-image {
  flex: 0 0 auto;
}

.tv-show-image img {
  display: block;
  max-width: 100%;
  height: auto;
}

.tv-show-info {
  flex: 1 1 auto;
}
</style>
