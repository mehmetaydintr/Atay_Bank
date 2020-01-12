import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import Header from './Header';
import {BackHandler} from 'react-native';
import {
  HeaderButtons,
  HeaderButton,
  Item,
} from 'react-navigation-header-buttons';
import React from 'react';
import axios from 'axios';
import CommonDataManager from './CommonDataManager';
import {Dropdown} from 'react-native-material-dropdown';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  TextInput,
  StatusBar,
  Button,
  Alert,
  Picker,
} from 'react-native';

import {
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
class KrediTahmini extends React.Component {
  state = {
    krediMiktari: '',
    Yas: '',
    evDurumu: '',
    status: '',
    telefonDurumu: '',
    krediSayisi: '',
  };
  TahminYap() {
    if (this.state.krediMiktari.length === 0) {
      Alert.alert('Kredi miktarı kısmı boş geçilemez');
    } else {
      if (this.state.Yas.length === 0) {
        Alert.alert('Yas kısmı boş geçilemez');
      } else {
        if (this.state.evDurumu.length === 0) {
          Alert.alert('Kredi miktarı kısmı boş geçilemez');
        } else {
          if (this.state.krediSayisi.length === 0) {
            Alert.alert('Kredi sayısı kısmı boş geçilemez');
          } else {
            if (this.state.telefonDurumu.length === 0) {
              Alert.alert('Telefon kısmı boş geçilemez');
            } else {
              axios({
                method: 'post',
                url: '104.41.129.46:5000/predict',
                data: {
                  miktar: this.state.krediMiktari,
                  yas: this.state.Yas,
                  ev: 1,
                  kredisayisi: this.state.krediSayisi,
                  tel: this.state.telefonDurumu,
                },
              }).then(response => {
                console.log(response);
              });
            }
          }
        }
      }
    }
  }
  render() {
    let items = [
      {
        value: 'Ev Sahibi',
      },
      {
        value: 'Kiracı',
      },
    ];
    let telefon = [
      {
        value: 'Var',
      },
      {
        value: 'Yok',
      },
    ];
    const {
      ViewStyle,
      subViewstyle,
      TextStyle,
      buttonStyle,
      DropdownStyle,
    } = styles;
    return (
      <>
        <SafeAreaView>
          <Header headerText="Kredi Tahmini" />
          <View style={ViewStyle}>
            <View style={subViewstyle}>
              <TextInput
                placeholder="Kredi Miktarı"
                style={TextStyle}
                value={this.state.krediMiktari}
                onChangeText={text => this.setState({krediMiktari: text})}
                maxLength={11}
                keyboardType={'numeric'}
              />
            </View>
            <View style={subViewstyle}>
              <TextInput
                placeholder="Yaş"
                style={TextStyle}
                value={this.state.Yas}
                onChangeText={text => this.setState({Yas: text})}
                maxLength={3}
                keyboardType={'numeric'}
              />
            </View>
            <View style={DropdownStyle}>
              <Dropdown
                label="Evinizin Durumu"
                data={items}
                onChangeText={text => this.setState({evDurumu: text})}
              />
            </View>
            <View style={subViewstyle}>
              <TextInput
                placeholder="Aldığınız Kredi Sayısı"
                style={TextStyle}
                value={this.state.krediSayisi}
                onChangeText={text => this.setState({krediSayisi: text})}
                maxLength={3}
                keyboardType={'numeric'}
              />
            </View>
            <View style={DropdownStyle}>
              <Dropdown
                label="Telefonunuz Var Mı ?"
                data={telefon}
                onChangeText={text => this.setState({telefonDurumu: text})}
              />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Kredi Durumumu Kontrol Et"
                onPress={() => this.TahminYap()}
              />
            </View>
            <View style={buttonStyle}>
              <Button
                title="Drawer"
                onPress={() => this.props.navigation.toggleDrawer()}
              />
            </View>
          </View>
        </SafeAreaView>
      </>
    );
  }
}
const styles = {
  ViewStyle: {
    borderWidth: 1,
    borderRadius: 2,
    borderColor: '#ddd',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'center',
  },
  buttonStyle: {
    flexDirection: 'row',
    marginRight: 5,
    marginLeft: 5,
    marginTop: 10,
    justifyContent: 'flex-end',
    position: 'relative',
  },
  subViewstyle: {
    borderWidth: 1,
    borderRadius: 20,
    borderColor: '#ddd',
    marginTop: 10,
    marginRight: 5,
    marginLeft: 5,
    padding: 5,
    justifyContent: 'flex-start',
    flexDirection: 'row',
    position: 'relative',
  },
  TextStyle: {
    color: 'black',
    fontSize: 20,
    flex: 2,
    paddingRight: 5,
    paddingLeft: 5,
  },
  DropdownStyle: {
    marginRight: 10,
    marginLeft: 10,
  },
};
export default KrediTahmini;
