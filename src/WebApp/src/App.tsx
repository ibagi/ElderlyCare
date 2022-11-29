import { RouterProvider } from 'react-router-dom'
import { I18nextProvider } from 'react-i18next'

import router from './router'
import i18n from './i18n'

import Layout from './components/Layout'
import './css/index.css'

export default function App() {
  return (
      <I18nextProvider i18n={i18n}>
        <Layout>
          <RouterProvider router={router}></RouterProvider>
        </Layout>
      </I18nextProvider>
  )
}