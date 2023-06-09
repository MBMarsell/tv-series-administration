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
    <div
      v-if="uploadedConfig && tvShows.length"
      class="filter-report-container"
    >
      <div>
        <label for="sort-by">Sort by:</label>
        <select v-model="sortBy" id="sort-by">
          <option value="name">Name</option>
          <option value="rating">Rating</option>
          <option value="genre">Genre</option>
        </select>
        <button @click="sortTvShows">Sort</button>
      </div>
      <div>
        <label for="reports">Reports:</label>
        <select v-model="selectedReport" id="reports">
          <option value="summary">Summary Report</option>
          <option value="top10">Top 10 shows Report</option>
        </select>
        <button @click="downloadReport" class="download-button">
          Download report
        </button>
      </div>
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
          <!-- <p>{{ tvShow?.summary }}</p> -->
          <p>
            Rating: {{ tvShow.rating?.average ? tvShow.rating.average : 'N/A' }}
          </p>
          <p>
            Genre:
            {{
              tvShow.genres && tvShow?.genres.length > 0
                ? tvShow.genres[0]
                : 'N/A'
            }}
          </p>
          <p>
            Network: {{ tvShow.network?.name ? tvShow.network.name : 'N/A' }}
          </p>
        </div>
        <button @click="selectedTVShow = tvShow">View Details</button>
        <TVShowModal
          v-if="selectedTVShow"
          :tvShow="selectedTVShow"
          @close="selectedTVShow = null"
        />
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios';
import TVShowModal from './TVShowModal.vue';

export default {
  components: {
    TVShowModal,
  },
  data() {
    return {
      tvShows: [],
      uploadedConfig: false,
      sortBy: 'name',
      fileSelected: false,
      selectedReport: 'summary',
      selectedTVShow: null,
    };
  },
  methods: {
    handleFileUpload() {
      const file = this.$refs.fileInput.files[0];
      const reader = new FileReader();
      reader.onload = () => {
        const tvShowTitles = reader.result.split('\n');
        const numTitles = tvShowTitles.length;
        let i = 0;
        const requestTitles = () => {
          for (let j = 0; j < 20 && i < numTitles; j++, i++) {
            this.getTVShowData(tvShowTitles[i].trim());
          }
          if (i < numTitles) {
            setTimeout(requestTitles, 11000);
          } else {
            this.uploadedConfig = true;
          }
        };
        requestTitles();
      };
      reader.readAsText(file);
    },
    async getTVShowData(title) {
      try {
        const res = await axios.get(
          'https://localhost:7150/api/TVShows/Show/',
          {
            params: {
              showTitle: title,
            },
          }
        );
        const tvShowData = res.data;
        tvShowData.showModal = false; // Init showModal to false
        this.tvShows.push(tvShowData);
        this.uploadedConfig = true;
        //   await new Promise((resolve) => setTimeout(resolve, 2000));
      } catch (err) {
        return null;
      }
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
    downloadReport() {
      let reportData = '';

      if (this.selectedReport === 'summary') {
        reportData += 'Name;Rating;Network;Genre\n';
        this.tvShows.forEach((tvShow) => {
          const rating = tvShow.rating?.average || 'N/A';
          const network = tvShow.network?.name || 'N/A';
          const genres =
            tvShow.genres && tvShow.genres.length > 0
              ? tvShow.genres[0]
              : 'N/A';
          reportData += `${tvShow.name};${rating};${network};${genres}\n`;
        });
      } else if (this.selectedReport === 'top10') {
        const top10Shows = this.tvShows
          .slice()
          .sort((a, b) => b.rating.average - a.rating.average)
          .slice(0, 10);

        reportData += `Name;Rating;Network;Genre\n`;
        top10Shows.forEach((tvShow) => {
          const rating = tvShow.rating?.average || 'N/A';
          const network = tvShow.network?.name || 'N/A';
          const genres =
            tvShow.genres && tvShow.genres.length > 0
              ? tvShow.genres[0]
              : 'N/A';
          reportData += `${tvShow.name};${rating};${network};${genres}\n`;
        });
      }

      const blob = new Blob([reportData], { type: 'text/plain' });
      const url = URL.createObjectURL(blob);
      const link = document.createElement('a');

      link.href = url;
      link.download = `${this.selectedReport}-report.txt`;

      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);
      URL.revokeObjectURL(url);
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
  padding: 10px;
}

.tv-show-item {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  align-items: center;
  justify-content: center;
  gap: 20px;
  padding: 20px;
  background-color: #f5f5f5;
  border-radius: 5px;
  border: 1px solid #ccc;
  width: 90%;
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

.filter-report-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  justify-content: center;
  margin: 2rem 0 2rem 0;
}
</style>
