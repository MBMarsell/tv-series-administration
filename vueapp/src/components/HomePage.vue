<template>
  <div>
    <div class="button-container">
      <button @click="showComponent('TVShow')" class="select-button">
        TV shows
      </button>
      <button @click="showComponent('Networks')" class="select-button">
        Networks
      </button>
    </div>
    <div v-if="selectedComponent === 'TVShow'">
      <TVShow />
    </div>

    <div v-if="selectedComponent === 'Networks'">
      <Networks />
    </div>
  </div>
</template>

<script>
import TVShow from './TVShow.vue';
import Networks from './Networks.vue';

export default {
  components: {
    TVShow,
    Networks,
  },
  data() {
    return {
      selectedComponent: '',
    };
  },
  methods: {
    showComponent(componentName) {
      this.selectedComponent = componentName;
    },
  },
};
</script>

<style scoped>
.button-container {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 2rem;
}
.select-button {
  font-size: medium;
  font-weight: bold;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}

.select-button:hover {
  scale: 1.1;
}
</style>
