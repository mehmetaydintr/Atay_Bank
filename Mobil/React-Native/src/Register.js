import {createAppContainer} from 'react-navigation';
import {createStackNavigator} from 'react-navigation-stack';
import React from 'react';
import axios from 'axios';
import Header from './Header';
import {BackHandler} from 'react-native';
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
} from 'react-native';
import CommonDataManager from './CommonDataManager';
import {
  LearnMoreLinks,
  Colors,
  DebugInstructions,
  ReloadInstructions,
} from 'react-native/Libraries/NewAppScreen';
class Register extends React.Component {
  static navigationOptions = {
    drawerLabel: () => null,
  };
  state = {
    musteriNo: '',
    TCKN: '',
    sifre: '',
    status: '',
    Ad: '',
    Soyad: '',
    email: '',
    Adres: '',
    Telefon: '',
  };
  KayıtOl() {
    let reg = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (this.state.TCKN.length === 11) {
      if (this.state.sifre.length === 6) {
        if (this.state.Ad.length === 0) {
          Alert.alert('Ad kısmı boş geçilemez');
        } else {
          if (this.state.Soyad.length === 0) {
            Alert.alert('Soyad kısmı boş geçilemez');
          } else {
            if (this.state.Adres.length === 0) {
              Alert.alert('Adres kısmı boş geçilemez');
            } else {
              if (this.state.Telefon.length === 11) {
                if (this.state.email.length === 0) {
                  Alert.alert('E-mail kısmı boş geçilemez');
                } else {
                  if (reg.test(this.state.email) === false) {
                    Alert.alert('Geçerli bir e-mail giriniz');
                  } else {
                    axios({
                      method: 'post',
                      url:
                        'https://tutunamayanlar.azurewebsites.net/api/tutunamayanlar/user/register',
                      data: {
                        TCKN: this.state.TCKN,
                        sifre: this.state.sifre,
                        email: this.state.email,
                        ad: this.state.Ad,
                        soyad: this.state.Soyad,
                        adres: this.state.Adres,
                        telefon: this.state.Telefon,
                      },
                    }).then(response => {
                      console.log(response.status);
                      this.setState({status: response.status});
                      if (this.state.status == '200') {
                        this.setState({musteriNo: response.data.musteriNo});
                        console.log(this.state.musteriNo);
                        let commonData = CommonDataManager.getInstance();
                        commonData.setMusteriNo(this.state.musteriNo);
                        this.props.navigation.navigate('Anasayfa', {
                          musteriNo: this.state.musteriNo,
                        });
                      } else {
                        this.setState({musteriNo: response.data.musteriNo});
                        Alert.alert(response.data);
                      }
                    });
                  }
                }
              } else {
                Alert.alert('Telefon kısmı boş geçilemez');
              }
            }
          }
        }
      } else {
        Alert.alert('Şifre 6 haneli olmalıdır');
      }
    } else {
      Alert.alert('TCKN 11 haneli olmalıdır');
    }
  }
  render() {
    const {ViewStyle, subViewstyle, TextStyle, buttonStyle} = styles;
    return (
      <>
        <Header headerText="Kayıt Ekranı" />
        <View style={ViewStyle}>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Ad"
              style={TextStyle}
              value={this.state.Ad}
              onChangeText={text => this.setState({Ad: text})}
              maxLength={20}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Soyad"
              style={TextStyle}
              value={this.state.Soyad}
              onChangeText={text => this.setState({Soyad: text})}
              maxLength={20}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="TCKN"
              style={TextStyle}
              value={this.state.TCKN}
              onChangeText={text => this.setState({TCKN: text})}
              maxLength={11}
              keyboardType={'numeric'}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="E-Mail"
              style={TextStyle}
              value={this.state.email}
              onChangeText={text => this.setState({email: text})}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Şifre"
              style={TextStyle}
              value={this.state.sifre}
              onChangeText={text => this.setState({sifre: text})}
              maxLength={6}
              keyboardType={'numeric'}
              secureTextEntry={true}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Adres"
              style={TextStyle}
              value={this.state.Adres}
              onChangeText={text => this.setState({Adres: text})}
            />
          </View>
          <View style={subViewstyle}>
            <TextInput
              placeholder="Telefon"
              style={TextStyle}
              value={this.state.Telefon}
              onChangeText={text => this.setState({Telefon: text})}
              maxLength={11}
              keyboardType={'numeric'}
            />
          </View>
          <Button
            style={buttonStyle}
            title="Kayıt Ol"
            onPress={() => this.KayıtOl()}
          />
        </View>
      </>
    );
  }
}
const styles = StyleSheet.create({
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
});
export default Register;
