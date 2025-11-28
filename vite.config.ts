import {PrimeVueResolver} from '@primevue/auto-import-resolver';
import tailwindcss from "@tailwindcss/vite";
import vue from "@vitejs/plugin-vue";
import path from "node:path";
import Components from 'unplugin-vue-components/vite';
import {defineConfig} from "vite";

const host = process.env.TAURI_DEV_HOST;

export default defineConfig(async () => ({
	plugins: [
		vue(),
		tailwindcss(),
		Components({
			resolvers: [PrimeVueResolver()]
		})
	],
	clearScreen: false,
	server: {
		port: 1420,
		strictPort: true,
		host: host || false,
		hmr: host ? {protocol: "ws", host, port: 1421} : undefined,
		watch: {
			ignored: ["**/src-tauri/**"],
		},
	},
	resolve: {
		alias: {
			'@': path.resolve(__dirname, './src'),
		},
	},
}));
