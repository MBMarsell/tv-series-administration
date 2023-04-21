<template>
  <div>
    <h2>Upload config to see TV shows</h2>
    <form @submit.prevent="handleFileUpload" class="upload-form">
      <input
        type="file"
        ref="fileInput"
        accept=".txt"
        class="hidden"
        id="fileInput"
        @change="fileSelected = true"
      />
      <label for="fileInput" class="upload-file-label">Select config</label>
      <button
        type="submit"
        class="upload-button"
        :class="{ disabledBtn: !fileSelected }"
      >
        Upload config
      </button>
    </form>
    <div v-if="uploadedConfig && tvShows.length">
      <label for="sort-by">Sort by:</label>
      <select v-model="sortBy" id="sort-by">
        <option value="name">Name</option>
        <option value="rating">Rating</option>
        <option value="genre">Genre</option>
      </select>
      <button @click="sortTvShows">Sort</button>
    </div>
    <ul class="tv-show-list">
      <li v-for="tvShow in tvShows" :key="tvShow.name" class="tv-show-item">
        <div class="tv-show">
          <div class="tv-show-image">
            <img :src="tvShow.image?.medium" :alt="tvShow?.name" />
          </div>
        </div>
        <div class="tv-show-info">
          <h2>{{ tvShow?.name }}</h2>
          <p>{{ tvShow?.summary }}</p>
          <p>Rating: {{ tvShow.rating?.average }}</p>
          <p>
            Genre:
            {{
              tvShow.genres && tvShow?.genres.length > 0
                ? tvShow.genres[0]
                : 'No genre found'
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
      sortBy: 'name',
      fileSelected: false,
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
      //   await new Promise((resolve) => setTimeout(resolve, 2000));
    },
    sortTvShows() {
      switch (this.sortBy) {
        case 'name':
          this.tvShows.sort((a, b) => {
            return a.name.localeCompare(b.name);
          });
          break;
        case 'rating':
          this.tvShows.sort((a, b) => {
            return b.rating.average - a.rating.average;
          });
          break;
        case 'genre':
          this.tvShows.sort((a, b) => {
            const genreA = a.genres && a.genres.length > 0 ? a.genres[0] : '';
            const genreB = b.genres && b.genres.length > 0 ? b.genres[0] : '';
            return genreA.localeCompare(genreB);
          });
        default:
          break;
      }
    },
  },
  computed: {
    sortedTvShows() {
      return this.tvShows;
    },
  },
};
</script>

<style scoped>
.hidden {
  display: none;
}

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

.upload-form {
  display: flex;
  align-items: center;
  gap: 10px;
  justify-content: center;
  margin: 2rem 0 2rem 0;
}

.upload-file-label {
  font-size: medium;
  font-weight: bold;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}

.upload-file-label:hover {
  background-color: #47ab26;
}

.upload-button {
  font-size: medium;
  font-weight: bold;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
  background-color: #47ab26;
}

.upload-button:hover {
  scale: 1.1;
}

.disabledBtn {
  pointer-events: none;
  cursor: not-allowed;
  opacity: 0.6;
  background-color: red;
}
</style>
