import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:joqds_quran/bloc/bloc.dart';

class ReadScreen extends StatelessWidget {
  const ReadScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SafeArea(
        child: BlocBuilder<AyahBloc, AyahState>(
          builder: (context, state) {
            if (state is UnAyahState) {
              return const Center(child: CircularProgressIndicator());
            }
            if (state is ErrorAyahState) {
              return Center(
                  child: Column(
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Text("خطایی رخ داده (${state.errorMessage})"),
                  ElevatedButton(
                      onPressed: () {
                        BlocProvider.of<AyahBloc>(context)
                            .add(LoadAyahBySurahEvent(state.surahId));
                      },
                      child: const Text("تلاش مجدد"))
                ],
              ));
            }
            if (state is InAyahState) {
              return ListView.builder(
                itemCount: state.ayat.length,
                itemBuilder: (context, index) {
                  var text = state.ayat[index].text!;
                  if (state.ayat[index].ayahInSurah != null) {
                    text += " (${state.ayat[index].ayahInSurah})";
                  }
                  return Padding(
                    padding: const EdgeInsets.all(8.0),
                    child: Text(text,
                        textAlign: TextAlign.center,
                        style: Theme.of(context).textTheme.headline5),
                  );
                },
              );
            }
            throw Exception();
          },
        ),
      ),
    );
  }
}
